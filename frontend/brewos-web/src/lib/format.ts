/**
 * Formats an amount in cents as a currency string (e.g. $3.50).
 */
export function formatCents(cents: number): string {
  return new Intl.NumberFormat('en-AU', {
    style: 'currency',
    currency: 'AUD',
  }).format(cents / 100)
}
