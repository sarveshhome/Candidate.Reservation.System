import { useState } from 'react'
import { CATEGORIES } from '../../shared/utils/constants'

function CandidateForm({ onCandidateAdded }) {
  const [candidate, setCandidate] = useState({
    candidateId: '',
    candidateName: '',
    category: 1,
    marks: ''
  })
  const [isSubmitting, setIsSubmitting] = useState(false)

  const handleSubmit = (e) => {
    e.preventDefault()
    setIsSubmitting(true)
    
    const candidateData = {
      ...candidate,
      marks: parseFloat(candidate.marks),
      timestamp: new Date().toISOString()
    }
    
    onCandidateAdded(candidateData)
    
    setCandidate({
      candidateId: '',
      candidateName: '',
      category: 1,
      marks: ''
    })
    
    setIsSubmitting(false)
  }

  return (
    <div className="card">
      <div className="card-header">
        <h5>Add Candidate</h5>
      </div>
      <div className="card-body">
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label className="form-label">Candidate ID</label>
            <input
              type="text"
              className="form-control"
              value={candidate.candidateId}
              onChange={(e) => setCandidate({...candidate, candidateId: e.target.value})}
              required
            />
          </div>
          
          <div className="mb-3">
            <label className="form-label">Name</label>
            <input
              type="text"
              className="form-control"
              value={candidate.candidateName}
              onChange={(e) => setCandidate({...candidate, candidateName: e.target.value})}
              required
            />
          </div>
          
          <div className="mb-3">
            <label className="form-label">Category</label>
            <select
              className="form-select"
              value={candidate.category}
              onChange={(e) => setCandidate({...candidate, category: parseInt(e.target.value)})}
            >
              {Object.entries(CATEGORIES).map(([value, label]) => (
                <option key={value} value={value}>{label}</option>
              ))}
            </select>
          </div>
          
          <div className="mb-3">
            <label className="form-label">Marks</label>
            <input
              type="number"
              step="0.1"
              className="form-control"
              value={candidate.marks}
              onChange={(e) => setCandidate({...candidate, marks: e.target.value})}
              required
            />
          </div>
          
          <button type="submit" className="btn btn-primary" disabled={isSubmitting}>
            {isSubmitting ? 'Processing...' : 'Submit Candidate'}
          </button>
        </form>
      </div>
    </div>
  )
}

export default CandidateForm