import { UserOutlined } from '@ant-design/icons';
import { Avatar, Card } from 'antd';
import React, { FC } from 'react';
import styled from 'styled-components';

interface IGroupCardProps {
  groupName?: string;
  isModerator?: boolean;
  moderatorName?: string;
  numberOfFiles?: number;
  numberOfUsers?: number;
  onClick: React.MouseEventHandler<HTMLDivElement>;
  uuid: string;
}

const GroupCard: FC<IGroupCardProps> = ({
  groupName,
  isModerator,
  moderatorName,
  numberOfFiles,
  numberOfUsers,
  onClick,
  uuid,
}) => {
  return (
    <StyledCard
      title={groupName}
      extra={isModerator && <StyledUserOutlined />}
      onClick={onClick}
      key={uuid}
    >
      <StyledTextWrapper>
        <div>Moderator:</div>
        <div>{moderatorName}</div>
      </StyledTextWrapper>
      <StyledTextWrapper>
        <div>Number of files:</div>
        <div>{numberOfFiles}</div>
      </StyledTextWrapper>
      <StyledTextWrapper>
        <div>Number of users:</div>
        <div>{numberOfUsers}</div>
      </StyledTextWrapper>

      <div
        style={{
          display: 'flex',
          gap: '10px',
        }}
      >
        <Avatar src="https://joeschmoe.io/api/v1/random" />
        <Avatar src="https://joeschmoe.io/api/v1/random" />
        <Avatar src="https://joeschmoe.io/api/v1/random" />
        <Avatar src="https://joeschmoe.io/api/v1/random" />
      </div>
    </StyledCard>
  );
};

const StyledCard = styled(Card).attrs({
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

const StyledUserOutlined = styled(UserOutlined)`
  color: red;
`;

const StyledTextWrapper = styled.div`
  display: flex;
  margin-bottom: 14px;
  justify-content: space-between;
`;

export default GroupCard;
