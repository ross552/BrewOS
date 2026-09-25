interface EmptyStateProps {
  title: string
  description: string
}

/**
 * Neutral empty-state message for missing catalog or data.
 */
export function EmptyState({ title, description }: EmptyStateProps) {
  return (
    <section
      className="border border-dashed border-stone-300 bg-stone-50/80 px-5 py-8 text-center"
      role="status"
      aria-live="polite"
    >
      <h2 className="text-sm font-semibold uppercase tracking-[0.18em] text-stone-700">
        {title}
      </h2>
      <p className="mt-2 text-sm text-stone-600">{description}</p>
    </section>
  )
}
