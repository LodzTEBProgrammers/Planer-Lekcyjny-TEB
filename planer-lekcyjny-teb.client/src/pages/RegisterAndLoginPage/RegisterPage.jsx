import { Link } from 'react-router-dom'

import styles from './Form.module.css'

function RegisterPage() {
  return (
    <form className={styles.form}>
      <header>
        <h1>Rejestracja</h1>
      </header>
      <input type="text" placeholder="Imie" required />
      <input type="email" placeholder="E-mail" required />
      <input type="password" placeholder="Hasło" required />
      <label>
        Zapamiętać <span>Hasło?</span>
        <input type="checkbox" />
      </label>
      <input type="submit" value="Register" className="login-btn" />
      <footer>
        Posiadasz konto?
        <Link to="/login">Zaloguj sie</Link>
      </footer>
    </form>
  )
}

export default RegisterPage
