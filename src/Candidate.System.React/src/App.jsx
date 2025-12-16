import CandidateForm from './features/candidate-form'
import SelectionResults from './features/selection-results'
import ReservationStats from './features/reservation-stats'
import Layout from './shared/components/Layout'
import { useCandidateData } from './shared/hooks/useCandidateData'

function App() {
  const { results, stats, addResult } = useCandidateData()

  return (
    <Layout>
      <div className="row">
        <div className="col-md-4">
          <CandidateForm onCandidateAdded={addResult} />
        </div>
        <div className="col-md-8">
          <ReservationStats stats={stats} />
          <SelectionResults results={results} />
        </div>
      </div>
    </Layout>
  )
}

export default App