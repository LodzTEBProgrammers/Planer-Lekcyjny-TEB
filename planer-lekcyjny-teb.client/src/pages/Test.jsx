import { useEffect } from 'react';
import { useAnnouncements } from '../hooks/useAnnouncements';

const Test = () => {
  const { announcements, addAnnouncement } = useAnnouncements();

  useEffect(() => {
    console.log(announcements);
  }, [announcements]);
  return (
    <h1
      onClick={() => {
        addAnnouncement({ content: 'Test' });
        console.log('test');
      }}
    >
      chujj
    </h1>
  );
};

export default Test;
