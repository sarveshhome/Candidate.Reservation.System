const categoryNames = {
  1: 'GENERAL',
  2: 'OBC',
  3: 'SC_ST',
  4: 'WOMAN',
  5: 'WOMAN_OBC',
  6: 'WOMAN_SC_ST'
}

function SelectionResults({ results }) {
  if (!results || results.length === 0) {
    return (
      <div className="card mt-3">
        <div className="card-header">
          <h5>Selection Results</h5>
        </div>
        <div className="card-body">
          <p className="text-muted">No selection results available</p>
        </div>
      </div>
    )
  }

  return (
    <div className="card mt-3">
      <div className="card-header">
        <h5>Selection Results ({results.length} selected)</h5>
      </div>
      <div className="card-body">
        <div className="table-responsive">
          <table className="table table-striped">
            <thead>
              <tr>
                <th>Candidate ID</th>
                <th>Name</th>
                <th>Category</th>
                <th>Marks</th>
                <th>Selected For</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {results.map((result, index) => (
                <tr key={index}>
                  <td>{result.candidateId}</td>
                  <td>{result.candidateName}</td>
                  <td>{categoryNames[result.category]}</td>
                  <td>{result.marks}</td>
                  <td>
                    <span className="badge bg-success">
                      {categoryNames[result.selectedForCategory]}
                    </span>
                  </td>
                  <td>
                    <span className="badge bg-primary">Selected</span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  )
}

export default SelectionResults