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

  return (
    <section className="border border-stone-200 bg-white px-5 py-4">
      <h2 className="text-sm font-semibold uppercase tracking-[0.18em] text-stone-700">
        Coffee menu
      </h2>
      <ul className="mt-4 divide-y divide-stone-100">
        {coffees.map((coffee) => {
          const isSelected = coffee.id === selectedCoffeeId
          const affordable = balanceInCents >= coffee.priceInCents

          return (
            <li key={coffee.id}>
              <button
                type="button"
                disabled={disabled || isPurchasing}
                onClick={() => onSelect(coffee.id)}
                className={`flex w-full items-center justify-between gap-4 px-2 py-3 text-left transition disabled:cursor-not-allowed disabled:opacity-50 ${
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

      <div className="mt-4 flex items-center justify-between gap-3 border-t border-stone-100 pt-4">
        <p className="text-sm text-stone-600">
          {selected
            ? `Selected: ${selected.name}`
            : 'Select a coffee to purchase'}
        </p>
        <button
          type="button"
          disabled={!selected || !canAfford || disabled || isPurchasing}
          onClick={onPurchase}
          className="bg-stone-900 px-4 py-2 text-sm font-medium text-white transition hover:bg-amber-900 disabled:cursor-not-allowed disabled:bg-stone-300"
        >
          {isPurchasing ? 'Brewing…' : 'Purchase'}
        </button>
      </div>
    </section>
  )
}
