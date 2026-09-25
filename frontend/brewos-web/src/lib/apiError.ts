import axios from 'axios'
import type { ApiErrorResponse } from '../types/apiError'

/**
 * Type guard for the backend ApiErrorResponse JSON shape.
 */
export function isApiErrorResponse(value: unknown): value is ApiErrorResponse {
  if (typeof value !== 'object' || value === null) {
    return false
  }

  const candidate = value as Record<string, unknown>
  return (
    typeof candidate.code === 'string' &&
    candidate.code.length > 0 &&
    typeof candidate.message === 'string' &&
    candidate.message.length > 0
  )
}

/**
 * Parses an Axios response body into ApiErrorResponse when present.
 */
export function parseApiErrorResponse(data: unknown): ApiErrorResponse | null {
  return isApiErrorResponse(data) ? data : null
}

/**
 * Maps known API error codes to stable UI copy when useful;
 * otherwise returns the backend message.
 */
function messageFromApiError(apiError: ApiErrorResponse): string {
  switch (apiError.code) {
    case 'INTERNAL_ERROR':
      return 'Unexpected server error occurred. Please try again.'
    case 'NOT_FOUND':
      return apiError.message || 'The requested resource was not found.'
    case 'INSUFFICIENT_BALANCE':
    case 'INVALID_ARGUMENT':
      return apiError.message
    default:
      return apiError.message
  }
}

/**
 * Fallback messages when the API did not return ApiErrorResponse.
 */
function messageFromStatus(status: number): string {
  switch (status) {
    case 400:
      return 'The request was invalid. Please check your input and try again.'
    case 404:
      return 'The requested resource was not found.'
    case 500:
      return 'Unexpected server error occurred. Please try again.'
    default:
      return `Request failed (${status}). Please try again.`
  }
}

/**
 * Extracts a user-facing message from unknown errors, preferring typed API errors.
 */
export function getErrorMessage(error: unknown): string {
  if (axios.isAxiosError(error)) {
    // No HTTP response → network / CORS / API offline
    if (!error.response) {
      if (error.code === 'ERR_NETWORK' || error.message === 'Network Error') {
        return 'Unable to reach the BrewOS API. Confirm the backend is running on http://localhost:5037.'
      }

      if (error.code === 'ECONNABORTED') {
        return 'The request timed out. Please try again.'
      }

      return 'Network request failed. Please check your connection and try again.'
    }

    const apiError = parseApiErrorResponse(error.response.data)
    if (apiError) {
      return messageFromApiError(apiError)
    }

    return messageFromStatus(error.response.status)
  }

  if (error instanceof Error && error.message) {
    return error.message
  }

  return 'Something went wrong. Please try again.'
}
