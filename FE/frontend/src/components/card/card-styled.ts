import { Button, Card } from 'antd';
import styled from 'styled-components';

// grid card
export const StyledGridCard = styled(Card).attrs({
  'data-testid': 'files-card',
})`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 25rem;
  border-radius: 1.5rem;
  overflow: hidden;
  max-width: 24rem;

  .ant-card-cover {
    height: 14rem;
    width: 24rem;
    overflow: hidden;
    border-top-left-radius: 1.5rem;
    border-top-right-radius: 1.5rem;
  }

  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

export const StyledMetaCardDescription = styled.div`
  .show-more {
    font-weight: bold;
    margin-left: 2rem;
  }
`;

// dashboard card

export const StyledDashboardCard = styled.div`
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 2rem;
  padding: 2rem;
  min-width: 22rem;
  flex-grow: 1;
`;

// group card

export const StyledGroupCard = styled(Card).attrs({
  'data-testid': 'group-card',
})`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 25rem;
  border-radius: 1.5rem;
  overflow: hidden;
  width: 24rem;

  .ant-card-cover {
    height: 14rem;
    width: 24rem;
    overflow: hidden;
    border-top-left-radius: 1.5rem;
    border-top-right-radius: 1.5rem;
  }

  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

export const StyledTextWrapper = styled.div`
  display: flex;
  margin-bottom: 14px;
  justify-content: space-between;
`;

// list card

export const StyledListCard = styled(Card)`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 7.5rem;
  border-radius: 1.5rem;
  overflow: hidden;

  .ant-card-cover {
    height: 14rem;
    width: 24rem;
    overflow: hidden;
    border-top-left-radius: 1.5rem;
    border-top-right-radius: 1.5rem;
  }

  .ant-card-body {
    padding: 1rem 2rem;
  }
`;
export const StyledListCardContainer = styled.div`
  width: 100%;
`;

export const StyledFilesButton = styled(Button)`
  border-radius: 0.8rem;
`;

export const StyledMetaDescription = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-top: 0.5rem;
`;

export const StyledDescriptionButtons = styled.div`
  display: flex;
  gap: 1rem;
`;

export const StyledDescriptionUploadDetails = styled.div`
  text-align: center;
`;
