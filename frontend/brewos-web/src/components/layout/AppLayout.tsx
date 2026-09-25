import type { ReactNode } from 'react'

interface AppLayoutProps {
  children: ReactNode
}

/**
 * Shared application shell for BrewOS pages.
 */
export function AppLayout({ children }: AppLayoutProps) {
  return (
    <div className="min-h-screen bg-gradient-to-b from-stone-100 via-stone-50 to-amber-50/40">
      <header className="border-b border-stone-200/80 bg-stone-950 text-stone-50">
        <div className="mx-auto flex max-w-5xl items-center justify-between px-6 py-5">
          <div>
            <p className="text-xs font-semibold uppercase tracking-[0.28em] text-amber-400">
              BrewOS
            </p>
            <h1 className="mt-1 text-xl font-semibold tracking-tight">
              Virtual Coffee Machine
            </h1>
          </div>
          <p className="hidden text-sm text-stone-400 sm:block">
            Insert coins · choose coffee · collect change
          </p>
        </div>
      </header>
      <main
        id="main-content"
        className="mx-auto max-w-5xl px-6 py-8"
        tabIndex={-1}
      >
        {children}
      </main>
    </div>
  )
}
