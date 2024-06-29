import { createContext, useState } from 'react'

import { apiUrl } from '../utils/config'

const AuthContext = createContext(undefined)

const AuthProvider = ({ children }) => {
  const [isLogged, setIsLogged] = useState(false) //Currently true for testing purposes

  //FIXME both register and login doesn't work idk why.
  const register = async (_userName, _email, _password, _name) => {
    try {
      const response = await fetch(`${apiUrl}/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          userName: _userName,
          email: _email,
          passwordHash: _password,
          name: _name,
        }),
      })

      if (!response.ok) console.log('problemo ' + response.statusText)
    } catch (error) {
      throw new Error('Error ', error)
    }
  }

  const login = async (_email, _password, _remember) => {
    try {
      const response = await fetch(`${apiUrl}/login`, {
        method: 'POST',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: _email,
          password: _password,
          remember: _remember,
        }),
      })

      if (!response.ok) console.log('problemo ' + response.statusText)

      console.log('logged in!')
    } catch (error) {
      throw new Error('Error ', error)
    }
  }

  return <AuthContext.Provider value={{ register, login, isLogged }}>{children}</AuthContext.Provider>
}

export { AuthContext, AuthProvider }
