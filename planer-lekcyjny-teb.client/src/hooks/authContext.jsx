import { createContext } from 'react'

import { apiUrl } from '../utils/config'

const AuthContext = createContext(undefined)

const AuthProvider = ({ children }) => {
  // const [isAdmin, setAdmin] = useState(false)

  const register = async (_userName, _email, _password, _name) => {
    try {
      fetch(`${apiUrl}/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ userName: _userName, email: _email, passwordHash: _password, name: _name }),
      })
    } catch (error) {
      throw new Error('Error ', error)
    }
  }

  return <AuthContext.Provider value={{ register }}>{children}</AuthContext.Provider>
}

export { AuthContext, AuthProvider }
