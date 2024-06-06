import { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import styled from '@emotion/styled';

function ErrorPage() {
  const [url, setUrl] = useState(String);

  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    setTimeout(() => {
      return navigate('/chuj');
    }, 5000);
  });

  useEffect(() => {
    setUrl(location.pathname.split('/')[1]);
  }, [url, location]);

  const StyledDiv = styled.div`
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
  `;

  return (
    <>
      <StyledDiv>
        <h1>Can I take your {url}?</h1>
        <h1>No its mine.</h1>
        <img src="https://media1.tenor.com/m/fcTjwtJ3ZysAAAAd/ibepoppingbottles-meme.gif" />
      </StyledDiv>
    </>
  );
}
export default ErrorPage;
