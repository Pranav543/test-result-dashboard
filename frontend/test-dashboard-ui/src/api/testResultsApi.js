import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5121/api',
})

export const testResultsApi = {
  /** Fetch all results, with optional filters applied as query params */
  getAll: (filters = {}) => {
    // Strip undefined/empty values so they don't appear as empty query params
    const params = Object.fromEntries(
      Object.entries(filters).filter(([, v]) => v !== '' && v != null)
    )
    return api.get('/testresults', { params })
  },

  getById: (id) => api.get(`/testresults/${id}`),

  getStats: () => api.get('/testresults/stats'),

  create: (data) => api.post('/testresults', data),

  delete: (id) => api.delete(`/testresults/${id}`),
}