import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { testResultsApi } from '../api/testResultsApi'

const EMPTY = {
  deviceId: '', deviceModel: '', testType: '',
  passed: true, measuredValue: '', expectedValue: '',
  tolerance: '', notes: '', technicianName: '',
}

function AddResult() {
  const [form,   setForm]   = useState(EMPTY)
  const [saving, setSaving] = useState(false)
  const [error,  setError]  = useState(null)
  const navigate = useNavigate()

  const set = (key, val) => setForm(f => ({ ...f, [key]: val }))

  const handleSubmit = async () => {
    if (!form.deviceId || !form.deviceModel || !form.testType || !form.technicianName) {
      setError('Please fill in all required fields.')
      return
    }
    setSaving(true)
    setError(null)
    try {
      await testResultsApi.create({
        ...form,
        measuredValue: form.measuredValue !== '' ? Number(form.measuredValue) : null,
        expectedValue: form.expectedValue !== '' ? Number(form.expectedValue) : null,
        tolerance:     form.tolerance     !== '' ? Number(form.tolerance)     : null,
      })
      navigate('/')
    } catch {
      setError('Failed to save result. Is the API running?')
    } finally {
      setSaving(false)
    }
  }

  const field = (label, key, type = 'text', required = false) => (
    <div>
      <label>{label}{required && <span style={{ color: 'var(--fail)' }}> *</span>}</label>
      <input
        type={type}
        value={form[key]}
        onChange={e => set(key, e.target.value)}
        placeholder={label}
      />
    </div>
  )

  return (
    <div style={{ maxWidth: 640, margin: '0 auto' }}>
      <div className="card" style={{ padding: '1.75rem' }}>
        <h2 style={{ fontSize: '1.15rem', fontWeight: 700, marginBottom: '1.5rem' }}>
          Log New Test Result
        </h2>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1rem' }}>
          {field('Device ID (Serial Number)', 'deviceId', 'text', true)}

          <div>
            <label>Device Model <span style={{ color: 'var(--fail)' }}>*</span></label>
            <select value={form.deviceModel} onChange={e => set('deviceModel', e.target.value)}>
              <option value="">— Select —</option>
              <option>SEL-300G</option>
              <option>SEL-400</option>
              <option>SEL-500</option>
            </select>
          </div>

          <div>
            <label>Test Type <span style={{ color: 'var(--fail)' }}>*</span></label>
            <select value={form.testType} onChange={e => set('testType', e.target.value)}>
              <option value="">— Select —</option>
              <option>Voltage</option>
              <option>Current</option>
              <option>Continuity</option>
              <option>Isolation</option>
            </select>
          </div>

          <div>
            <label>Result <span style={{ color: 'var(--fail)' }}>*</span></label>
            <select value={String(form.passed)} onChange={e => set('passed', e.target.value === 'true')}>
              <option value="true">✓ Passed</option>
              <option value="false">✗ Failed</option>
            </select>
          </div>

          {field('Measured Value', 'measuredValue', 'number')}
          {field('Expected Value', 'expectedValue', 'number')}
          {field('Tolerance (±)',  'tolerance',     'number')}
          {field('Technician Name', 'technicianName', 'text', true)}
        </div>

        <div style={{ marginTop: '1rem' }}>
          <label>Notes / Failure Description</label>
          <textarea
            rows={3}
            value={form.notes}
            onChange={e => set('notes', e.target.value)}
            placeholder="Optional notes about this test..."
            style={{ resize: 'vertical' }}
          />
        </div>

        {error && (
          <p style={{ color: 'var(--fail)', fontSize: '.875rem', marginTop: '.75rem' }}>{error}</p>
        )}

        <div style={{ display: 'flex', gap: '.75rem', marginTop: '1.25rem' }}>
          <button className="btn-primary" onClick={handleSubmit} disabled={saving}>
            {saving ? 'Saving…' : 'Save Result'}
          </button>
          <button className="btn-ghost" onClick={() => navigate('/')}>Cancel</button>
        </div>
      </div>
    </div>
  )
}

export default AddResult