import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { sidebarHandleVisibility } from '../../../redux/actions/sidebarActions';
import { removeStoryRequest } from '../../../redux/actions/storiesActions';
import { getSelectedStory } from '../../../redux/selectors/storiesSelectors';
import { IStory } from '../../../types/storyTypes';
import SidebarStoryRemove, { ISidebarStoryRemoveProps } from './SidebarStoryRemove';

const SidebarStoryRemoveContainer = () => {
    const dispatch = useDispatch();

    const story: IStory = useSelector(getSelectedStory);

    const onClickRemoveStory = (): void => {
        dispatch(removeStoryRequest(story.storyId, story.recordVersion));
    };

    const onClickCancelRemove = (): void => {
        dispatch(sidebarHandleVisibility(null, false));
    };

    const sidebarStoryRemove: ISidebarStoryRemoveProps = {
        story,
        onClickRemoveStory,
        onClickCancelRemove,
    };

    return <SidebarStoryRemove {...sidebarStoryRemove} />;
};

export default SidebarStoryRemoveContainer;
