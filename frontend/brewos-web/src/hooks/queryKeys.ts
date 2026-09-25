export const coffeeMachineKeys = {
  all: ['coffeeMachine'] as const,
  coffees: () => [...coffeeMachineKeys.all, 'coffees'] as const,
  status: () => [...coffeeMachineKeys.all, 'status'] as const,
}
