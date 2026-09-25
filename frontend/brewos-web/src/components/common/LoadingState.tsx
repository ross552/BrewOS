interface LoadingStateProps {
  label?: string
}

/**
 * Lightweight loading indicator for async machine state.
 */
export function LoadingState({ label = 'Loading…' }: LoadingStateProps) {
  return (
    <div className="flex items-center gap-3 py-6 text-sm text-stone-500" role="status">
      <span
        className="inline-block h-4 w-4 animate-spin rounded-full border-2 border-stone-300 border-t-amber-700"
        aria-hidden
      />
      <span>{label}</span>
    </div>
  )
}
