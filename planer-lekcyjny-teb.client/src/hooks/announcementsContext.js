import { createContext, useEffect, useState } from 'react'

import { apiUrl } from '../utils/config'

const AnnouncementsContext = createContext(undefined)

export const AnnouncementsProvider = ({ children }) => {
  const [announcements, setAnnouncements] = useState(null)

  const getAnnouncements = async () => {
    try {
      const response = await fetch(`${apiUrl}/admin/announcement`, {
        method: 'GET',
      })
      setAnnouncements(await response.json())
    } catch (error) {
      throw new Error('Error ', error)
    }
  }

  const addAnnouncement = ({ content }) => {
    if (content) {
      try {
        fetch(`${apiUrl}/admin/announcement`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ content: content }),
        })
        getAnnouncements()
      } catch (error) {
        throw new Error('Error ', error)
      }
    }
  }

  useEffect(() => {
    getAnnouncements()
  }, [])

  return <AnnouncementsContext.Provider value={{ announcements, addAnnouncement }}>{children}</AnnouncementsContext.Provider>
}

export { AnnouncementsContext }
