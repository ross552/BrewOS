interface ErrorBannerProps {
  message: string
  onDismiss?: () => void
}

/**
 * Inline error banner for machine workflow failures.
 */
export function ErrorBanner({ message, onDismiss }: ErrorBannerProps) {
  return (
    <div
      role="alert"
      className="flex items-start justify-between gap-4 border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-900"
    >
      <p>{message}</p>
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
