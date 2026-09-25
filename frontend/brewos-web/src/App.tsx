import { Route, Routes } from 'react-router-dom'
import { AppLayout } from './components/layout/AppLayout'
import { MachinePage } from './pages/MachinePage'

function App() {
  return (
    <AppLayout>
      <Routes>
        <Route path="/" element={<MachinePage />} />
      </Routes>
    </AppLayout>
  )
}

export default App
