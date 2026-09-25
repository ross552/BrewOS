import axios from 'axios'

/**
 * Shared Axios instance for BrewOS API calls.
 */
export const apiClient = axios.create({
  baseURL: 'http://localhost:5037',
  headers: {
    'Content-Type': 'application/json',
  },
})
