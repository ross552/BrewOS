import axios from 'axios'
import { useState } from 'react'
import { EmptyState } from '../components/common/EmptyState'
import { ErrorBanner } from '../components/common/ErrorBanner'
import { LoadingState } from '../components/common/LoadingState'
import { BalanceDisplay } from '../components/machine/BalanceDisplay'
import { CoffeeMenu } from '../components/machine/CoffeeMenu'
import { CoinPad } from '../components/machine/CoinPad'
import { PurchaseResultPanel } from '../components/machine/PurchaseResultPanel'
import { useCoffees } from '../hooks/useCoffees'
import { useInsertCoin } from '../hooks/useInsertCoin'
import { useMachineStatus } from '../hooks/useMachineStatus'
import { usePurchaseCoffee } from '../hooks/usePurchaseCoffee'
import { getErrorMessage, parseApiErrorResponse } from '../lib/apiError'
import type { CoinDenomination, PurchaseResult } from '../types/coffeeMachine'

interface UiError {
  message: string
  code?: string
}

function toUiError(error: unknown): UiError {
  const message = getErrorMessage(error)
  if (axios.isAxiosError(error)) {
    const apiError = parseApiErrorResponse(error.response?.data)
    return { message, code: apiError?.code }
  }

  return { message }
}

/**
 * Main coffee machine workflow page.
 */
export function MachinePage() {
  const coffeesQuery = useCoffees()
  const statusQuery = useMachineStatus()
  const insertCoin = useInsertCoin()
  const purchaseCoffee = usePurchaseCoffee()

  const [selectedCoffeeId, setSelectedCoffeeId] = useState<string | null>(null)
  const [lastPurchase, setLastPurchase] = useState<PurchaseResult | null>(null)
  const [actionError, setActionError] = useState<UiError | null>(null)

  const balanceInCents = statusQuery.data?.balanceInCents ?? 0
  const isInserting = insertCoin.isPending
  const isPurchasing = purchaseCoffee.isPending
  const isBusy = isInserting || isPurchasing

  const loadError =
    coffeesQuery.error || statusQuery.error
      ? toUiError(coffeesQuery.error ?? statusQuery.error)
      : null

  const displayedError = actionError ?? loadError
  const coffees = coffeesQuery.data ?? []

  const handleInsertCoin = (valueInCents: CoinDenomination) => {
    if (isBusy) {
      return
    }

    setActionError(null)
    setLastPurchase(null)
    insertCoin.mutate(valueInCents, {
      onError: (error) => setActionError(toUiError(error)),
    })
  }

  const handleSelectCoffee = (coffeeId: string) => {
    if (isBusy) {
      return
    }

    setActionError(null)
    setLastPurchase(null)
    setSelectedCoffeeId(coffeeId)
  }

  const handlePurchase = () => {
    if (!selectedCoffeeId || isBusy) {
      return
    }

    setActionError(null)
    setLastPurchase(null)
    purchaseCoffee.mutate(selectedCoffeeId, {
      onSuccess: (result) => {
        setLastPurchase(result)
        setSelectedCoffeeId(null)
      },
      onError: (error) => setActionError(toUiError(error)),
    })
  }

  return (
    <div className="space-y-5">
      <div>
        <h2 className="text-2xl font-semibold tracking-tight text-stone-900">
          Machine console
        </h2>
        <p className="mt-1 text-sm text-stone-600">
          Insert supported coins, choose a drink, and collect your change.
        </p>
      </div>

      {displayedError ? (
        <ErrorBanner
          message={displayedError.message}
          code={displayedError.code}
          onDismiss={
            actionError
              ? () => setActionError(null)
              : undefined
          }
        />
      ) : null}

      {lastPurchase ? (
        <PurchaseResultPanel
          result={lastPurchase}
          onDismiss={() => setLastPurchase(null)}
        />
      ) : null}

      <BalanceDisplay
        balanceInCents={balanceInCents}
        isLoading={statusQuery.isLoading}
        isUpdating={isInserting}
      />

      <CoinPad
        disabled={Boolean(loadError) || isPurchasing}
        isPending={isInserting}
        onInsert={handleInsertCoin}
      />

      {coffeesQuery.isLoading ? (
        <LoadingState label="Loading coffee menu…" />
      ) : coffees.length > 0 ? (
        <CoffeeMenu
          coffees={coffees}
          balanceInCents={balanceInCents}
          selectedCoffeeId={selectedCoffeeId}
          disabled={Boolean(loadError) || isInserting}
          isPurchasing={isPurchasing}
          onSelect={handleSelectCoffee}
          onPurchase={handlePurchase}
        />
      ) : !loadError ? (
        <EmptyState
          title="No coffees available"
          description="The machine catalog is empty. Please try again once drinks are configured."
        />
      ) : null}
    </div>
  )
}
