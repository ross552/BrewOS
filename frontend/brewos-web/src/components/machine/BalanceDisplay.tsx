import { motion } from 'framer-motion'
import { formatCents } from '../../lib/format'

interface BalanceDisplayProps {
  balanceInCents: number
  isLoading?: boolean
}

/**
 * Displays the current inserted balance.
 */
export function BalanceDisplay({ balanceInCents, isLoading }: BalanceDisplayProps) {
  return (
    <section className="border border-stone-200 bg-white px-5 py-4">
      <p className="text-xs font-semibold uppercase tracking-[0.2em] text-stone-500">
        Current balance
      </p>
      <motion.p
        key={balanceInCents}
        initial={{ opacity: 0.4, y: 4 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.2 }}
        className="mt-2 text-3xl font-semibold tracking-tight text-stone-900"
      >
        {isLoading ? '—' : formatCents(balanceInCents)}
      </motion.p>
    </section>
  )
}
