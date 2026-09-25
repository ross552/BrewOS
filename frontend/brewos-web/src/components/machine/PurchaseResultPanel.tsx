import { motion } from 'framer-motion'
import type { PurchaseResult } from '../../types/coffeeMachine'
import { formatCents } from '../../lib/format'

interface PurchaseResultPanelProps {
  result: PurchaseResult
  onDismiss: () => void
}

/**
 * Shows a successful purchase and returned change.
 */
export function PurchaseResultPanel({ result, onDismiss }: PurchaseResultPanelProps) {
  return (
    <motion.section
      initial={{ opacity: 0, y: 8 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.25 }}
      className="border border-emerald-200 bg-emerald-50 px-5 py-4"
      role="status"
      aria-live="polite"
      aria-atomic="true"
      aria-labelledby="purchase-result-heading"
    >
      <div className="flex items-start justify-between gap-4">
        <div>
          <p className="text-xs font-semibold uppercase tracking-[0.2em] text-emerald-800">
            Purchase complete
          </p>
          <h2
            id="purchase-result-heading"
            className="mt-2 text-xl font-semibold text-stone-900"
          >
            Enjoy your {result.coffee.name}
          </h2>
          <p className="mt-1 text-sm text-stone-700">
            Paid {formatCents(result.coffee.priceInCents)}
          </p>
        </div>
        <button
          type="button"
          onClick={onDismiss}
          aria-label="Dismiss purchase success message"
          className="text-sm font-medium text-emerald-900 underline-offset-2 hover:underline focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-emerald-800"
        >
          Dismiss
        </button>
      </div>

      <div className="mt-4 border-t border-emerald-200/80 pt-3">
        <p className="text-sm font-medium text-stone-800">
          Change: {formatCents(result.changeTotalInCents)}
        </p>
        {result.change.length > 0 ? (
          <p className="mt-1 text-sm text-stone-600">
            Coins returned:{' '}
            {result.change.map((coin) => formatCents(coin)).join(', ')}
          </p>
        ) : (
          <p className="mt-1 text-sm text-stone-600">Exact payment — no change.</p>
        )}
      </div>
    </motion.section>
  )
}
