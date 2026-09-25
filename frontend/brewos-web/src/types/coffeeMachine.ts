/**
 * Coffee product offered by the virtual machine.
 */
export interface Coffee {
  id: string
  name: string
  priceInCents: number
}

/**
 * Current machine balance status.
 */
export interface MachineStatus {
  balanceInCents: number
}

/**
 * Outcome of a successful coffee purchase.
 */
export interface PurchaseResult {
  coffee: Coffee
  change: number[]
  changeTotalInCents: number
}

/**
 * Supported coin denominations in cents.
 */
export const SUPPORTED_COINS = [5, 10, 20, 50, 100, 200] as const

export type CoinDenomination = (typeof SUPPORTED_COINS)[number]
