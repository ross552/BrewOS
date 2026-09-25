import { SUPPORTED_COINS, type CoinDenomination } from '../../types/coffeeMachine'
import { formatCents } from '../../lib/format'

interface CoinPadProps {
  disabled?: boolean
  isPending?: boolean
  onInsert: (valueInCents: CoinDenomination) => void
}

/**
 * Coin insertion pad for supported denominations.
 */
export function CoinPad({ disabled, isPending, onInsert }: CoinPadProps) {
  const isDisabled = Boolean(disabled || isPending)

  return (
    <section
      className="border border-stone-200 bg-white px-5 py-4"
      aria-labelledby="coin-pad-heading"
      aria-busy={isPending || undefined}
    >
      <div className="flex items-baseline justify-between gap-3">
        <h2
          id="coin-pad-heading"
          className="text-sm font-semibold uppercase tracking-[0.18em] text-stone-700"
        >
          Insert coin
        </h2>
        {isPending ? (
          <span className="text-xs text-amber-800" role="status" aria-live="polite">
            Updating balance…
          </span>
        ) : null}
      </div>
      <div
        className="mt-4 grid grid-cols-3 gap-2 sm:grid-cols-6"
        role="group"
        aria-label="Supported coin denominations"
      >
        {SUPPORTED_COINS.map((coin) => (
          <button
            key={coin}
            type="button"
            disabled={isDisabled}
            aria-label={`Insert ${formatCents(coin)} coin`}
            aria-disabled={isDisabled}
            onClick={() => {
              if (isDisabled) {
                return
              }
              onInsert(coin)
            }}
            className="border border-stone-300 bg-stone-50 px-2 py-3 text-sm font-medium text-stone-800 transition hover:border-amber-700 hover:bg-amber-50 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-amber-800 disabled:cursor-not-allowed disabled:opacity-50"
          >
            {formatCents(coin)}
          </button>
        ))}
      </div>
    </section>
  )
}
