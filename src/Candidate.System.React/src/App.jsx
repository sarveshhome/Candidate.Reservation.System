import { useState } from 'react'
import CandidateForm from './components/CandidateForm.jsx'
import SelectionResults from './components/SelectionResults.jsx'
import ReservationStats from './components/ReservationStats.jsx'

function App() {
  const [results, setResults] = useState([])
  const [stats, setStats] = useState({})

  const addResult = (candidate) => {
    const newResult = {
      ...candidate,
      selectedForCategory: candidate.category
    }
    setResults(prev => [...prev, newResult])
    
    setStats(prev => ({
      ...prev,
      [candidate.category]: (prev[candidate.category] || 0) + 1,
      totalCandidates: (prev.totalCandidates || 0) + 1,
      totalSelected: (prev.totalSelected || 0) + 1
    }))
  }

  return (
    <div className="container-fluid">
      <nav className="navbar navbar-dark bg-primary mb-4">
        <div className="container">
          <span className="navbar-brand">Candidate Selection System</span>
        </div>
      </nav>
      
      <div className="row">
        <div className="col-md-4">
          <CandidateForm onCandidateAdded={addResult} />
        </div>
        <div className="col-md-8">
          <ReservationStats stats={stats} />
          <SelectionResults results={results} />
        </div>
      </div>
    </div>
  )
}

export default App