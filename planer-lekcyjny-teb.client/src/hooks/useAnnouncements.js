import { useContext } from 'react'

import { AnnouncementsContext } from './announcementsContext'

export const useAnnouncements = () => {
  const context = useContext(AnnouncementsContext)
  if (!context) {
    throw new Error('useAnnouncement should be wrapped in AnnouncementsProvider')
  }
  
  return context
}
