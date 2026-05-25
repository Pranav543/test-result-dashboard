import { useState, useEffect, useCallback } from 'react'
import { testResultsApi } from '../api/testResultsApi'
import StatsCard from '../components/StatsCard'
import PassFailChart from '../components/PassFailChart'
import TrendChart from '../components/TrendChart'
import FilterBar from '../components/FilterBar'
import TestResultTable from '../components/TestResultTable'

function Dashboard() {
  const [stats,   setStats]   = useState(null)
  const [results, setResults] = useState([])
  const [filters, setFilters] = useState({})
  const [loading, setLoading] = useState(true)
  const [error,   setError]   = useState(null)

  // Fetch aggregate stats once on mount
  useEffect(() => {
    testResultsApi.getStats()
      .then(r => setStats(r.data))
      .catch(() => setError('Failed to load stats.'))
  }, [])

  // Re-fetch results whenever filters change
  const fetchResults = useCallback(() => {
    setLoading(true)
    testResultsApi.getAll(filters)
      .then(r => { setResults(r.data); setError(null) })
      .catch(() => setError('Failed to load results.'))
      .finally(() => setLoading(false))
  }, [filters])

  useEffect(() => { fetchResults() }, [fetchResults])

  const handleDeleted = (id) => {
    setResults(prev => prev.filter(r => r.id !== id))
    // Refresh stats after deletion
    testResultsApi.getStats().then(r => setStats(r.data))
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '1.5rem' }}>

      {/* ── Stats Cards ─────────────────────────────────────────────────── */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(180px, 1fr))', gap: '1rem' }}>
        <StatsCard title="TOTAL TESTS"  value={stats?.total   ?? '—'} accentColor="var(--primary)" />
        <StatsCard title="PASSED"       value={stats?.passed  ?? '—'} accentColor="var(--pass)"
          subtitle={stats ? `${stats.passRate}% pass rate` : undefined} />
        <StatsCard title="FAILED"       value={stats?.failed  ?? '—'} accentColor="var(--fail)" />
        <StatsCard title="PASS RATE"    value={stats ? `${stats.passRate}%` : '—'} accentColor="#7c3aed"
          subtitle={`${results.length} results shown`} />
      </div>

      {/* ── Charts ──────────────────────────────────────────────────────── */}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 2fr', gap: '1rem' }}>
        {stats && <PassFailChart passed={stats.passed} failed={stats.failed} />}
        {results.length > 0 && <TrendChart results={results} />}
      </div>

      {/* ── Filter + Table ───────────────────────────────────────────────── */}
      <div className="card" style={{ padding: '1.25rem' }}>
        <h3 style={{ fontSize: '.9rem', fontWeight: 600, marginBottom: '1rem', color: 'var(--text-muted)' }}>
          TEST RESULTS
        </h3>
        <FilterBar filters={filters} onChange={setFilters} />

        {error   && <p className="state-message" style={{ color: 'var(--fail)' }}>{error}</p>}
        {loading && <p className="state-message">Loading…</p>}
        {!loading && !error && (
          <TestResultTable results={results} onDeleted={handleDeleted} />
        )}
      </div>
    </div>
  )
}

export default Dashboard