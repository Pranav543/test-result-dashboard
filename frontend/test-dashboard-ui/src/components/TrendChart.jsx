import { useMemo } from 'react'
import {
  LineChart, Line, XAxis, YAxis, CartesianGrid,
  Tooltip, Legend, ResponsiveContainer
} from 'recharts'

function TrendChart({ results }) {
  const data = useMemo(() => {
    const map = {}
    results.forEach(r => {
      const day = new Date(r.testedAt).toLocaleDateString('en-US', { month: 'short', day: 'numeric' })
      if (!map[day]) map[day] = { date: day, Passed: 0, Failed: 0 }
      if (r.passed) map[day].Passed++
      else          map[day].Failed++
    })
    return Object.values(map)
  }, [results])

  return (
    <div className="card" style={{ padding: '1.25rem' }}>
      <h3 style={{ fontSize: '.9rem', fontWeight: 600, marginBottom: '1rem', color: 'var(--text-muted)' }}>
        DAILY TEST TREND
      </h3>
      <ResponsiveContainer width="100%" height={220}>
        <LineChart data={data} margin={{ right: 20 }}>
          <CartesianGrid strokeDasharray="3 3" stroke="var(--border)" />
          <XAxis dataKey="date" tick={{ fontSize: 11 }} />
          <YAxis allowDecimals={false} tick={{ fontSize: 11 }} />
          <Tooltip />
          <Legend />
          <Line type="monotone" dataKey="Passed" stroke="#16a34a" strokeWidth={2} dot={{ r: 4 }} />
          <Line type="monotone" dataKey="Failed" stroke="#dc2626" strokeWidth={2} dot={{ r: 4 }} />
        </LineChart>
      </ResponsiveContainer>
    </div>
  )
}

export default TrendChart