import { RESERVATION_CONFIG } from '../../shared/utils/constants'

function ReservationStats({ stats }) {
  return (
    <div className="card">
      <div className="card-header">
        <h5>Reservation Statistics</h5>
      </div>
      <div className="card-body">
        <div className="row">
          {Object.entries(RESERVATION_CONFIG).map(([category, config]) => {
            const selected = stats[category] || 0
            const total = stats.totalCandidates || 0
            const percentage = total > 0 ? ((selected / total) * 100).toFixed(1) : 0
            
            return (
              <div key={category} className="col-md-6 mb-3">
                <div className={`card border-${config.color}`}>
                  <div className="card-body text-center">
                    <h6 className={`card-title text-${config.color}`}>
                      {config.name}
                    </h6>
                    <div className="d-flex justify-content-between">
                      <small>Reserved: {config.percentage}%</small>
                      <small>Selected: {selected}</small>
                    </div>
                    <div className="progress mt-2">
                      <div
                        className={`progress-bar bg-${config.color}`}
                        style={{ width: `${Math.min(percentage, 100)}%` }}
                      >
                        {percentage}%
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            )
          })}
        </div>
        
        {stats.totalCandidates > 0 && (
          <div className="mt-3 text-center">
            <h6>Total Candidates: {stats.totalCandidates}</h6>
            <h6>Total Selected: {stats.totalSelected || 0}</h6>
          </div>
        )}
      </div>
    </div>
  )
}

export default ReservationStats