import { useQuery } from '@tanstack/react-query'
import { coffeeMachineApi } from '../api/coffeeMachineApi'
import { coffeeMachineKeys } from './queryKeys'

/**
 * Loads the current machine balance.
 */
export function useMachineStatus() {
  return useQuery({
    queryKey: coffeeMachineKeys.status(),
    queryFn: () => coffeeMachineApi.getStatus(),
  })
}
