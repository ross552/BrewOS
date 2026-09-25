interface ErrorBannerProps {
  message: string
  /** Optional machine-readable API error code (e.g. INSUFFICIENT_BALANCE). */
  code?: string
  onDismiss?: () => void
}

/**
 * Inline error banner for machine workflow failures.
 */
export function ErrorBanner({ message, code, onDismiss }: ErrorBannerProps) {
  return (
    <div
      role="alert"
      className="flex items-start justify-between gap-4 border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-900"
    >
      <div>
        {code ? (
          <p className="text-xs font-semibold uppercase tracking-[0.16em] text-red-700">
            {code.replace(/_/g, ' ')}
          </p>
        ) : null}
        <p className={code ? 'mt-1' : undefined}>{message}</p>
      </div>
      {onDismiss ? (
        <button
          type="button"
          onClick={onDismiss}
          className="shrink-0 font-medium text-red-700 underline-offset-2 hover:underline"
        >
          Dismiss
        </button>
      ) : null}
    </div>
  )
}
