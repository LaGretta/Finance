const API_URL = 'http://localhost:5253/api';

function getToken() {
  return localStorage.getItem('token');
}

async function request(method, path, body = null, auth = false) {
  const headers = { 'Content-Type': 'application/json' };
  if (auth) headers['Authorization'] = `Bearer ${getToken()}`;

  const res = await fetch(`${API_URL}${path}`, {
    method,
    headers,
    body: body ? JSON.stringify(body) : null,
  });

  if (res.status === 204) return null;

  const data = await res.json().catch(() => null);

  if (!res.ok) {
    const msg = Array.isArray(data)
      ? data.join(', ')
      : data?.message || data?.title || 'Something went wrong';
    throw new Error(msg);
  }

  return data;
}

// Auth
const Auth = {
  register: (username, password) =>
    request('POST', '/auth/register', { username, password }),

  login: (username, password) =>
    request('POST', '/auth/login', { username, password }),
};

// Expenses
const Expenses = {
  getAll: () =>
    request('GET', '/expenses', null, true),

  create: (title, category, amount) =>
    request('POST', '/expenses', { title, category, amount: parseFloat(amount) }, true),

  delete: (id) =>
    request('DELETE', `/expenses/${id}`, null, true),
};
