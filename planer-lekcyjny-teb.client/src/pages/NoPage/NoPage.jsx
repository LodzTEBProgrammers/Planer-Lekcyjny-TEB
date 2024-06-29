import { useEffect, useState } from 'react'
import { useLocation, useNavigate } from 'react-router-dom'

import styles from './NoPage.module.css'

function NoPage() {
  const [url, setUrl] = useState(String)

  const location = useLocation()
  const navigate = useNavigate()

  useEffect(() => {
    setTimeout(() => {
      return navigate('/')
    }, 5000)
  })

  useEffect(() => {
    setUrl(location.pathname.split('/')[1])
  }, [url, location])

  return (
    <div className={styles.div}>
      <h1>Can I take your {url}?</h1>
      <h1>No its mine.</h1>
      <img src="https://media1.tenor.com/m/fcTjwtJ3ZysAAAAd/ibepoppingbottles-meme.gif" />
    </div>
  )
}
export default NoPage
