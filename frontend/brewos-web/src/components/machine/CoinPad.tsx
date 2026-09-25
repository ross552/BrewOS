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
  return (
    <section className="border border-stone-200 bg-white px-5 py-4">
      <div className="flex items-baseline justify-between gap-3">
        <h2 className="text-sm font-semibold uppercase tracking-[0.18em] text-stone-700">
          Insert coin
        </h2>
        {isPending ? (
          <span className="text-xs text-amber-800">Updating balance…</span>
        ) : null}
      </div>
      <div className="mt-4 grid grid-cols-3 gap-2 sm:grid-cols-6">
        {SUPPORTED_COINS.map((coin) => (
          <button
            key={coin}
            type="button"
            disabled={disabled || isPending}
            onClick={() => onInsert(coin)}
            className="border border-stone-300 bg-stone-50 px-2 py-3 text-sm font-medium text-stone-800 transition hover:border-amber-700 hover:bg-amber-50 disabled:cursor-not-allowed disabled:opacity-50"
          >
            {formatCents(coin)}
          </button>
        ))}
      </div>
    </section>
  )
}
