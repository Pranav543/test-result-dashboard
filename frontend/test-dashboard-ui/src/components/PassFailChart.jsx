import { PieChart, Pie, Cell, Tooltip, Legend, ResponsiveContainer } from 'recharts'

const COLORS = { Passed: '#16a34a', Failed: '#dc2626' }

function PassFailChart({ passed, failed }) {
  const data = [
    { name: 'Passed', value: passed },
    { name: 'Failed', value: failed },
  ].filter(d => d.value > 0)

  return (
    <div className="card" style={{ padding: '1.25rem' }}>
      <h3 style={{ fontSize: '.9rem', fontWeight: 600, marginBottom: '1rem', color: 'var(--text-muted)' }}>
        PASS / FAIL BREAKDOWN
      </h3>
      <ResponsiveContainer width="100%" height={220}>
        <PieChart>
          <Pie
            data={data}
            cx="50%"
            cy="50%"
            innerRadius={55}
            outerRadius={85}
            paddingAngle={3}
            dataKey="value"
          >
            {data.map(entry => (
              <Cell key={entry.name} fill={COLORS[entry.name]} />
            ))}
          </Pie>
          <Tooltip formatter={(v) => [v, 'Tests']} />
          <Legend />
        </PieChart>
      </ResponsiveContainer>
    </div>
  )
}

export default PassFailChart