import { Link } from 'react-router-dom'

import styles from './Form.module.css'

function LoginPage() {
  return (
    <form className={styles.form}>
      <header>
        <h1>Logowanie</h1>
      </header>
      <input type="email" placeholder="Email" required />
      <input type="password" placeholder="Hasło" required />
      <label>
        Zapamiętać <span>Hasło</span>
        <input type="checkbox" />
      </label>
      <input type="submit" value="Login" className="login-btn" />
      <footer>
        Nie masz konta?
        <Link to="/register">Zarejestruj sie</Link>
      </footer>
    </form>
  )
}

export default LoginPage
