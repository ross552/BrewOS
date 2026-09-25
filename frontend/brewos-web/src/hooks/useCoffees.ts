import { useQuery } from '@tanstack/react-query'
import { coffeeMachineApi } from '../api/coffeeMachineApi'
import { coffeeMachineKeys } from './queryKeys'

/**
 * Loads the available coffee menu.
 */
export function useCoffees() {
  return useQuery({
    queryKey: coffeeMachineKeys.coffees(),
    queryFn: () => coffeeMachineApi.getCoffees(),
  })
}
