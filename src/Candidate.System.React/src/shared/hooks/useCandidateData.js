import { useState } from 'react';

export const useCandidateData = () => {
  const [results, setResults] = useState([]);
  const [stats, setStats] = useState({});

  const addResult = (candidate) => {
    const newResult = {
      ...candidate,
      selectedForCategory: candidate.category
    };
    setResults(prev => [...prev, newResult]);
    
    setStats(prev => ({
      ...prev,
      [candidate.category]: (prev[candidate.category] || 0) + 1,
      totalCandidates: (prev.totalCandidates || 0) + 1,
      totalSelected: (prev.totalSelected || 0) + 1
    }));
  };

  return { results, stats, addResult };
};