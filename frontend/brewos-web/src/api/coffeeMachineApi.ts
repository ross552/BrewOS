import { apiClient } from './client'
import type { Coffee, MachineStatus, PurchaseResult } from '../types/coffeeMachine'

interface PurchaseResultResponse {
  coffee: Coffee
  changeInCents: number[]
  changeTotalInCents: number
}

/**
 * BrewOS coffee machine API surface.
 */
export const coffeeMachineApi = {
  async getCoffees(): Promise<Coffee[]> {
    const { data } = await apiClient.get<Coffee[]>('/api/coffees')
    return data
  },

  async insertCoin(valueInCents: number): Promise<MachineStatus> {
    const { data } = await apiClient.post<MachineStatus>('/api/machine/coins', {
      valueInCents,
    })
    return data
  },

  async getStatus(): Promise<MachineStatus> {
    const { data } = await apiClient.get<MachineStatus>('/api/machine/status')
    return data
  },

  async purchaseCoffee(coffeeId: string): Promise<PurchaseResult> {
    const { data } = await apiClient.post<PurchaseResultResponse>(
      '/api/machine/purchase',
      { coffeeId },
    )

    return {
      coffee: data.coffee,
      change: data.changeInCents,
      changeTotalInCents: data.changeTotalInCents,
    }
  },
}
