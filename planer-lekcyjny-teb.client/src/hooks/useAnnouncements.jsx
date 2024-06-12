import { createContext, useContext, useEffect, useState } from 'react'

const AnnouncementContext = createContext(undefined)

export const AnnouncementsProvider = ({ children }) => {
  const [announcements, setAnnouncements] = useState(null)

  const getAnnouncements = async () => {
    try {
      const response = await fetch('http://localhost:5083/api/SecureWebsite/admin/announcement', { method: 'GET' })
      setAnnouncements(await response.json())
    } catch (error) {
      throw new Error('Error ', error)
    }
  }

  const addAnnouncement = ({ content }) => {
    if (content) {
      try {
        fetch('http://localhost:5083/api/SecureWebsite/admin/announcement', {
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

  return <AnnouncementContext.Provider value={{ announcements, addAnnouncement }}>{children}</AnnouncementContext.Provider>
}

export const useAnnouncements = () => {
  const context = useContext(AnnouncementContext)
  if (!context) throw new Error('useAnnouncement should be wrapped in AnnouncementsProvider')
  return context
}
