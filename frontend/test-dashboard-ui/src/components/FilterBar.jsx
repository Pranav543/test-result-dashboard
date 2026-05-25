const MODELS    = ['', 'SEL-300G', 'SEL-400', 'SEL-500']
const TEST_TYPES = ['', 'Voltage', 'Current', 'Continuity', 'Isolation']

function FilterBar({ filters, onChange }) {
  const set = (key, value) => onChange({ ...filters, [key]: value })

  return (
    <div
      style={{
        display: 'grid',
        gridTemplateColumns: 'repeat(auto-fit, minmax(160px, 1fr))',
        gap: '.75rem',
        marginBottom: '1rem',
      }}
    >
      <div>
        <label>Device Model</label>
        <select value={filters.deviceModel ?? ''} onChange={e => set('deviceModel', e.target.value)}>
          <option value="">All Models</option>
          {MODELS.filter(Boolean).map(m => <option key={m}>{m}</option>)}
        </select>
      </div>

      <div>
        <label>Test Type</label>
        <select value={filters.testType ?? ''} onChange={e => set('testType', e.target.value)}>
          <option value="">All Types</option>
          {TEST_TYPES.filter(Boolean).map(t => <option key={t}>{t}</option>)}
        </select>
      </div>

      <div>
        <label>Status</label>
        <select
          value={filters.passed === undefined ? '' : String(filters.passed)}
          onChange={e => set('passed', e.target.value === '' ? undefined : e.target.value === 'true')}
        >
          <option value="">All</option>
          <option value="true">Passed</option>
          <option value="false">Failed</option>
        </select>
      </div>

      <div>
        <label>From Date</label>
        <input type="date" value={filters.from ?? ''} onChange={e => set('from', e.target.value)} />
      </div>

      <div>
        <label>To Date</label>
        <input type="date" value={filters.to ?? ''} onChange={e => set('to', e.target.value)} />
      </div>

      <div style={{ display: 'flex', alignItems: 'flex-end' }}>
        <button className="btn-ghost" style={{ width: '100%' }}
          onClick={() => onChange({})}>
          Clear Filters
        </button>
      </div>
    </div>
  )
}

export default FilterBar