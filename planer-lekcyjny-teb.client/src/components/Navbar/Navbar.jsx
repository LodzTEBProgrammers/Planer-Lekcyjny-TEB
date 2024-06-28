import styles from './Navbar.module.css'

function Navbar() {
  const isLogged = false

  return (
    <nav className={styles.nav}>
      {isLogged ? (
        <ul>
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="/admin">Admin</a>
          </li>
          <li>
            <a href="/plan-lekcji">Plan Lekcji</a>
          </li>
          <li>Log out</li>
        </ul>
      ) : (
        <ul>
          <li>
            <a href="/login">Login</a>
          </li>
          <li>
            <a href="/register">Register</a>
          </li>
          <li>
            <a href="/plan-lekcji">Plan Lekcji</a>
          </li>
          <li>
            <a href="/ogloszenia">Ogłoszenia</a>
          </li>
        </ul>
      )}
    </nav>
  )
}

export default Navbar
