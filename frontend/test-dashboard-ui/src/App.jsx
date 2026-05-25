import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom'
import Dashboard from './pages/Dashboard'
import AddResult from './pages/AddResult'
import './App.css'

function App() {
  return (
    <BrowserRouter>
      <div className="app-shell">
        <header className="app-header">
          <div className="header-brand">
            <span className="brand-icon">⚡</span>
            <span className="brand-name">TestDash</span>
            <span className="brand-sub">Factory Test Dashboard</span>
          </div>
          <nav className="header-nav">
            <NavLink to="/" end className={({ isActive }) => isActive ? 'nav-link active' : 'nav-link'}>
              Dashboard
            </NavLink>
            <NavLink to="/add" className={({ isActive }) => isActive ? 'nav-link active' : 'nav-link'}>
              + Log Result
            </NavLink>
          </nav>
        </header>

        <main className="app-main">
          <Routes>
            <Route path="/"    element={<Dashboard />} />
            <Route path="/add" element={<AddResult />} />
          </Routes>
        </main>
      </div>
    </BrowserRouter>
  )
}

export default App