import { useState } from 'react'
import { testResultsApi } from '../api/testResultsApi'

function TestResultTable({ results, onDeleted }) {
  const [deletingId, setDeletingId] = useState(null)

  const handleDelete = async (id) => {
    if (!confirm('Delete this test result?')) return
    setDeletingId(id)
    try {
      await testResultsApi.delete(id)
      onDeleted(id)
    } catch {
      alert('Failed to delete record.')
    } finally {
      setDeletingId(null)
    }
  }

  if (results.length === 0) {
    return <p className="state-message">No results match your filters.</p>
  }

  return (
    <div style={{ overflowX: 'auto' }}>
      <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '.875rem' }}>
        <thead>
          <tr style={{ borderBottom: '2px solid var(--border)', textAlign: 'left' }}>
            {['Date', 'Device ID', 'Model', 'Test Type', 'Measured', 'Expected ± Tol', 'Status', 'Technician', 'Notes', ''].map(h => (
              <th key={h} style={{ padding: '.6rem .75rem', fontWeight: 600, fontSize: '.75rem',
                color: 'var(--text-muted)', whiteSpace: 'nowrap' }}>{h}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {results.map(r => (
            <tr key={r.id}
              style={{ borderBottom: '1px solid var(--border)', transition: 'background .1s' }}
              onMouseEnter={e => e.currentTarget.style.background = 'var(--bg)'}
              onMouseLeave={e => e.currentTarget.style.background = ''}
            >
              <td style={td}>{new Date(r.testedAt).toLocaleDateString()}</td>
              <td style={{ ...td, fontFamily: 'monospace', fontSize: '.82rem' }}>{r.deviceId}</td>
              <td style={td}>{r.deviceModel}</td>
              <td style={td}>{r.testType}</td>
              <td style={td}>{r.measuredValue ?? '—'}</td>
              <td style={td}>
                {r.expectedValue != null
                  ? `${r.expectedValue} ± ${r.tolerance ?? 0}`
                  : '—'}
              </td>
              <td style={td}>
                <span className={r.passed ? 'badge badge-pass' : 'badge badge-fail'}>
                  {r.passed ? '✓ Pass' : '✗ Fail'}
                </span>
              </td>
              <td style={td}>{r.technicianName}</td>
              <td style={{ ...td, maxWidth: 180, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>
                {r.notes ?? ''}
              </td>
              <td style={td}>
                <button className="btn-danger" style={{ fontSize: '.75rem', padding: '.25rem .6rem' }}
                  disabled={deletingId === r.id}
                  onClick={() => handleDelete(r.id)}>
                  {deletingId === r.id ? '…' : 'Del'}
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

const td = { padding: '.6rem .75rem', verticalAlign: 'middle' }

export default TestResultTable