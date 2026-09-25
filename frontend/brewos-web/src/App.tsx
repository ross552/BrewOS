import { motion } from 'framer-motion'
import { Route, Routes } from 'react-router-dom'

function HomePage() {
  return (
    <main className="flex min-h-screen items-center justify-center bg-stone-50 px-6">
      <motion.div
        initial={{ opacity: 0, y: 12 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.4 }}
        className="max-w-xl text-center"
      >
        <p className="text-sm font-medium uppercase tracking-[0.2em] text-amber-800">
          BrewOS
        </p>
        <h1 className="mt-3 text-4xl font-semibold tracking-tight text-stone-900">
          Virtual Coffee Machine Platform
        </h1>
        <p className="mt-4 text-base text-stone-600">
          Frontend foundation is ready. Domain models, API endpoints, and
          machine workflows will be added in later steps.
        </p>
      </motion.div>
    </main>
  )
}

function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
    </Routes>
  )
}

export default App
