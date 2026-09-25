import { motion } from 'framer-motion'
import { formatCents } from '../../lib/format'

interface BalanceDisplayProps {
  balanceInCents: number
  isLoading?: boolean
  isUpdating?: boolean
}

/**
 * Displays the current inserted balance.
 */
export function BalanceDisplay({
  balanceInCents,
  isLoading,
  isUpdating,
}: BalanceDisplayProps) {
  const displayValue = isLoading ? '—' : formatCents(balanceInCents)

  return (
    <section
      className="border border-stone-200 bg-white px-5 py-4"
      aria-labelledby="balance-heading"
      aria-busy={isLoading || isUpdating || undefined}
    >
      <p
        id="balance-heading"
        className="text-xs font-semibold uppercase tracking-[0.2em] text-stone-500"
      >
        Current balance
      </p>
      <motion.p
        key={displayValue}
        initial={{ opacity: 0.4, y: 4 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.2 }}
        className="mt-2 text-3xl font-semibold tracking-tight text-stone-900"
        aria-live="polite"
        aria-atomic="true"
      >
        {displayValue}
      </motion.p>
      {isUpdating ? (
        <p className="mt-1 text-xs text-amber-800" role="status">
          Updating…
        </p>
      ) : null}
    </section>
  )
}
