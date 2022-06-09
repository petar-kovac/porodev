import { Avatar, List } from 'antd';
import React from 'react';
import styled from 'styled-components';
import dayjs from 'dayjs';

// const data = [
//   {
//     title: 'Ant Design Title 1',
//   },
//   {
//     title: 'Ant Design Title 2',
//   },
//   {
//     title: 'Ant Design Title 3',
//   },
//   {
//     title: 'Ant Design Title 4',
//   },
// ];

const PList: React.FC<{ data?: unknown[] | undefined }> = ({ data }) => {
  return (
    <List
      itemLayout="horizontal"
      dataSource={data}
      renderItem={(item: any) => (
        <List.Item>
          <List.Item.Meta
            avatar={<Avatar src={item.avatar} />}
            title={<a href="https://ant.design">{item.name}</a>}
            description={
              <StyledDescription>
                <div>{item.address}</div>
                <div style={{ marginRight: 450, textAlign: 'left' }}>
                  {dayjs.unix(Date.parse(item.createdAt)).format('D MMMM')}
                </div>
              </StyledDescription>
            }
          />
        </List.Item>
      )}
    />
  );
};

const StyledDescription = styled.div`
  display: flex;
  justify-content: space-between;
`;

export default PList;
