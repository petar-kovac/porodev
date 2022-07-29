import { Button } from 'antd';
import styled from 'styled-components';

export const StyledFilterWrapper = styled.div`
  width: 100%;

  /* background-color: #fcfcfc;
  border-radius: 3rem;
  box-shadow: 1px 3px 8px rgba(34, 25, 25, 0.2);
  border-bottom: 2px solid #ddd; */
`;

export const StyledPageWrapper = styled.div`
  height: 100%;
  display: flex;
  flex-direction: column;
`;

export const StyledContent = styled.div`
  display: flex;
  flex: 1;
`;

export const StyledStaticContent = styled.div`
  position: relative;
  /* width: 100%;
  margin-top: 4rem;
  padding: 2rem 4rem;
  display: flex;
  flex-direction: column;
  gap: 5rem; */
  width: 100%;
  padding: 2rem 4rem;
  display: flex;
  flex-direction: column;
  /* align-items: center; */
  gap: 5rem;
  margin-top: 4rem;
`;

export const StyledFoldersContainer = styled.div`
  display: flex;
  flex-direction: column;

  & h2 {
    font-size: 3rem;
  }
`;
export const StyledHeadingWrapper = styled.div`
  display: flex;
  justify-content: space-between;
`;

export const StyledFilesWrapper = styled.div`
  //   display: grid;
  //   grid-template-columns: repeat(auto-fit, minmax(210px, 16%));
  //   gap: 1rem;
  //   padding-left: 2rem;
`;

// export const StyledFoldersWrapper = styled.div`
//   display: flex;
//   flex-wrap: wrap;
//   padding-left: 2rem;

//   gap: 2rem;
// `;
export const StyledFoldersWrapper = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(210px, 16%));
  gap: 1rem;
  padding-left: 2rem;
`;

export const StyledFilesContainer = styled.div`
  display: flex;
  flex-direction: column;

  & h2 {
    font-size: 3rem;
  }
`;

export const StyledPlusCircle = styled(Button)`
  width: 5.5rem;
  height: 5.5rem;
  position: absolute;
  bottom: 5rem;
  right: 5rem;
`;
