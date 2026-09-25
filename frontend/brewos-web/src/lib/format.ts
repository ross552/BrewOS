import axios from 'axios'

/**
 * Formats an amount in cents as a currency string (e.g. $3.50).
 */
export function formatCents(cents: number): string {
  return new Intl.NumberFormat('en-AU', {
    style: 'currency',
    currency: 'AUD',
  }).format(cents / 100)
}

/**
 * Extracts a user-facing message from an unknown error (including Axios API errors).
 */
export function getErrorMessage(error: unknown): string {
  if (axios.isAxiosError(error)) {
    const detail = error.response?.data?.detail
    if (typeof detail === 'string' && detail.length > 0) {
      return detail
    }

    if (error.response?.status === 404) {
      return 'The requested coffee was not found.'
    }

    if (error.message) {
      return error.message
    }
  }

  if (error instanceof Error) {
    return error.message
  }

  return 'Something went wrong. Please try again.'
}
