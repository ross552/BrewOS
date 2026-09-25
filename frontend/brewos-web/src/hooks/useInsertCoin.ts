import { useMutation, useQueryClient } from '@tanstack/react-query'
import { coffeeMachineApi } from '../api/coffeeMachineApi'
import type { MachineStatus } from '../types/coffeeMachine'
import { coffeeMachineKeys } from './queryKeys'

/**
 * Inserts a coin and updates cached machine status.
 */
export function useInsertCoin() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (valueInCents: number) => coffeeMachineApi.insertCoin(valueInCents),
    onSuccess: (status: MachineStatus) => {
      queryClient.setQueryData(coffeeMachineKeys.status(), status)
    },
  })
}
