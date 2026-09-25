import { useMutation, useQueryClient } from '@tanstack/react-query'
import { coffeeMachineApi } from '../api/coffeeMachineApi'
import { coffeeMachineKeys } from './queryKeys'

/**
 * Purchases a coffee and resets cached balance to zero on success.
 */
export function usePurchaseCoffee() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (coffeeId: string) => coffeeMachineApi.purchaseCoffee(coffeeId),
    onSuccess: () => {
      queryClient.setQueryData(coffeeMachineKeys.status(), { balanceInCents: 0 })
    },
  })
}
