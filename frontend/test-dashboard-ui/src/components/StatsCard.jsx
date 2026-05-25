function StatsCard({ title, value, subtitle, accentColor }) {
  return (
    <div
      className="card"
      style={{
        padding: '1.25rem 1.5rem',
        borderTop: `3px solid ${accentColor}`,
      }}
    >
      <p style={{ fontSize: '.8rem', fontWeight: 600, color: 'var(--text-muted)', marginBottom: '.5rem' }}>
        {title}
      </p>
      <p style={{ fontSize: '2rem', fontWeight: 700, lineHeight: 1 }}>
        {value}
      </p>
      {subtitle && (
        <p style={{ fontSize: '.8rem', color: 'var(--text-muted)', marginTop: '.4rem' }}>
          {subtitle}
        </p>
      )}
    </div>
  )
}

export default StatsCard