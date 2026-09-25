import type { Coffee } from '../../types/coffeeMachine'
import { formatCents } from '../../lib/format'

interface CoffeeMenuProps {
  coffees: Coffee[]
  balanceInCents: number
  selectedCoffeeId: string | null
  disabled?: boolean
  isPurchasing?: boolean
  onSelect: (coffeeId: string) => void
  onPurchase: () => void
}

/**
 * Coffee catalog with selection and purchase action.
 */
export function CoffeeMenu({
  coffees,
  balanceInCents,
  selectedCoffeeId,
  disabled,
  isPurchasing,
  onSelect,
  onPurchase,
}: CoffeeMenuProps) {
  const selected = coffees.find((coffee) => coffee.id === selectedCoffeeId)
  const canAfford = selected ? balanceInCents >= selected.priceInCents : false
  const interactionsLocked = Boolean(disabled || isPurchasing)
  const purchaseDisabled =
    !selected || !canAfford || interactionsLocked

  const handlePurchaseClick = () => {
    if (purchaseDisabled) {
      return
    }
    onPurchase()
  }

  return (
    <section
      className="border border-stone-200 bg-white px-5 py-4"
      aria-labelledby="coffee-menu-heading"
      aria-busy={isPurchasing || undefined}
    >
      <h2
        id="coffee-menu-heading"
        className="text-sm font-semibold uppercase tracking-[0.18em] text-stone-700"
      >
        Coffee menu
      </h2>

      <ul className="mt-4 divide-y divide-stone-100" role="listbox" aria-label="Available coffees">
        {coffees.map((coffee) => {
          const isSelected = coffee.id === selectedCoffeeId
          const affordable = balanceInCents >= coffee.priceInCents

          return (
            <li key={coffee.id} role="option" aria-selected={isSelected}>
              <button
                type="button"
                disabled={interactionsLocked}
                aria-pressed={isSelected}
                aria-label={`Select ${coffee.name}, price ${formatCents(coffee.priceInCents)}${
                  affordable ? ', enough balance' : ', needs more coins'
                }`}
                onClick={() => {
                  if (interactionsLocked) {
                    return
                  }
                  onSelect(coffee.id)
                }}
                className={`flex w-full items-center justify-between gap-4 px-2 py-3 text-left transition focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-amber-800 disabled:cursor-not-allowed disabled:opacity-50 ${
                  isSelected
                    ? 'bg-amber-50 ring-1 ring-inset ring-amber-700'
                    : 'hover:bg-stone-50'
                }`}
              >
                <span>
                  <span className="block font-medium text-stone-900">{coffee.name}</span>
                  <span className="mt-0.5 block text-xs text-stone-500">
                    {affordable ? 'Enough balance' : 'Needs more coins'}
                  </span>
                </span>
                <span className="font-semibold text-stone-800">
                  {formatCents(coffee.priceInCents)}
                </span>
              </button>
            </li>
          )
        })}
      </ul>

      <div className="mt-4 flex flex-wrap items-center justify-between gap-3 border-t border-stone-100 pt-4">
        <p className="text-sm text-stone-600" aria-live="polite">
          {isPurchasing
            ? 'Processing purchase…'
            : selected
              ? `Selected: ${selected.name}`
              : 'Select a coffee to purchase'}
        </p>
        <button
          type="button"
          disabled={purchaseDisabled}
          aria-disabled={purchaseDisabled}
          aria-busy={isPurchasing || undefined}
          aria-label={
            isPurchasing
              ? 'Purchase in progress'
              : selected
                ? `Purchase ${selected.name}`
                : 'Purchase selected coffee'
          }
          onClick={handlePurchaseClick}
          className="inline-flex items-center gap-2 bg-stone-900 px-4 py-2 text-sm font-medium text-white transition hover:bg-amber-900 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-amber-800 disabled:cursor-not-allowed disabled:bg-stone-300"
        >
          {isPurchasing ? (
            <>
              <span
                className="inline-block h-3.5 w-3.5 animate-spin rounded-full border-2 border-white/40 border-t-white"
                aria-hidden="true"
              />
              Brewing…
            </>
          ) : (
            'Purchase'
          )}
        </button>
      </div>
    </section>
  )
}
