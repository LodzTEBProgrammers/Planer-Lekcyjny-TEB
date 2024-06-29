import { Link } from 'react-router-dom'

import styles from './Navbar.module.css'

const links = [
  { name: 'Home', to: '/' },
  { name: 'Ogłoszenia', to: '/ogloszenia' },
  { name: 'Login', to: '/login' },
]

const adminLinks = [
  { name: 'Home', to: '/' },
  { name: 'Admin', to: '/admin' },
]

function Navbar() {
  const isLogged = true

  return (
    <nav className={styles.nav}>
      {/* Admin navbar */}
      {isLogged && (
        <ul>
          {adminLinks.map((link, index) => (
            <li key={index}>
              <Link to={link.to}>{link.name}</Link>
            </li>
          ))}

          <li style={{ color: '#ff0900' }}>Log out</li>
        </ul>
      )}

      {/* No user navbar */}
      {!isLogged && (
        <ul>
          {links.map((link, index) => (
            <li key={index}>
              <Link to={link.to}>{link.name}</Link>
            </li>
          ))}
        </ul>
      )}
    </nav>
  )
}

export default Navbar
